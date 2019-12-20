import * as React from 'react';
import Event from '../../components/event/event';
import EventCategory from '../../components/event/event-category';
import EventsService from '../../services/events-service';
import { IRootState } from '../../store';
import { Dispatch } from 'redux';
import * as actions from '../../store/actions';
import { AppActions } from '../../store/types';
import { connect } from 'react-redux';
import { GetEventRequest, Filter, SortBy, StatusFilter, DateFilter, CategoryFilter } from '../events/get-events-request'
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import './search-bar.css';

const mapStateToProps = ({ app }: IRootState) => {
  const { events, filter, sort, eventCategories } = app;
  return { events, filter, sort, eventCategories };
}

type ReduxType = ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatcherToProps>;

const mapDispatcherToProps = (dispatch: Dispatch<AppActions>) => {
  return {
    setEvents: (events: Array<Event>) => dispatch(actions.setEvents(events)),
    setEventCategories: (eventCategories: Array<EventCategory>) => dispatch(actions.setEventCategories(eventCategories)),
    filterEvents: (filter: Filter) => dispatch(actions.filterEvents(filter)),
    sortEvents: (sort: SortBy) => dispatch(actions.sortEvents(sort)),
    setPageCount: (pageCount: number) => dispatch(actions.setPageCount(pageCount))
  }
}

interface IState {
  sortOrder: number,
  sortType: number,
  filterStatus: string
}

class SearchBar extends React.Component<ReduxType, IState> {
  private _eventService: EventsService;

  constructor(props: any) {
    super(props);
    this._eventService = new EventsService();
    this.onStatusChange = this.onStatusChange.bind(this);
    this.onDateChange = this.onDateChange.bind(this);
    this.onOrderChange = this.onOrderChange.bind(this);
    this.onOrderDirectionChange = this.onOrderDirectionChange.bind(this);
    this.onCategorySelected = this.onCategorySelected.bind(this);

    this.state = { sortOrder: 2, sortType: 2, filterStatus: 'all' };
  }

  public async componentDidMount(): Promise<void> {

    var response = await this._eventService.getEvents(new GetEventRequest());
    this.props.setEvents(response.events);
    this.props.setPageCount(response.pageCount);

    var eventCategories = await this._eventService.getEventCategories();
    this.props.setEventCategories(eventCategories);

    if (this.props.filter.categoryFilter === null)
      this.props.filter.categoryFilter = new CategoryFilter();

    this.props.filter.categoryFilter.categoryIds = eventCategories.map(function (a) { return a.id; });

  }

  public async onStatusChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { sort, filter } = this.props;
    var val = e.target.value;
    if (filter.statusFilter === null)
      filter.statusFilter = new StatusFilter();

    if (val === "closed")
      filter.statusFilter.isClosed = true;
    else if (val === "open")
      filter.statusFilter.isClosed = false;
    else
      filter.statusFilter = null;

    await this.setEvents(sort, filter);
  }

  public async onDateChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { sort, filter } = this.props;
    var val = e.target.value;
    if (e.target.name === "dateFrom") {
      if (filter.dateFilter === null)
        filter.dateFilter = new DateFilter();

      if (val === "") filter.dateFilter.from = null;
      else filter.dateFilter.from = val;
    }
    else if (e.target.name === "dateTo") {
      if (filter.dateFilter === null)
        filter.dateFilter = new DateFilter();

      if (val === "") filter.dateFilter.to = null;
      else filter.dateFilter.to = val;
    }

    await this.setEvents(sort, filter);

  }

  public async onCategorySelected(e: React.ChangeEvent<HTMLInputElement>) {
    const { sort, filter } = this.props;

    if (filter.categoryFilter === null)
      filter.categoryFilter = new CategoryFilter();

    var categoryId = +e.target.value;
    var categorySelected = e.target.checked;

    if (!categorySelected) {
      const index = filter.categoryFilter.categoryIds.indexOf(categoryId, 0);
      if (index > -1) {
        filter.categoryFilter.categoryIds.splice(index, 1);
      }
    }
    else {
      filter.categoryFilter.categoryIds.push(categoryId);
    }

    await this.setEvents(sort, filter);
  }

  public async onOrderChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { sort, filter } = this.props;
    var val = e.target.value;

    if (val === "date")
      sort.sortType = 1;
    else if (val === "status")
      sort.sortType = 2;
    else
      sort.sortType = 3;

    await this.setEvents(sort, filter);
  }

  public async onOrderDirectionChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { sort, filter } = this.props;
    var val = e.target.value;

    if (val === "asc")
      sort.sortOrder = 1;    
    else
      sort.sortOrder = 2;

    await this.setEvents(sort, filter);
  }

  private async setEvents(sort: SortBy, filter: Filter) {
    var request = new GetEventRequest();
    request.sortBy = sort;
    request.filter = filter;
    var result = await this._eventService.getEvents(request);
    this.props.setEvents(result.events);
  }

  render() {
    const {sortOrder, sortType, filterStatus} = this.state;
    
    const { eventCategories } = this.props;
    return (
      <div>
        <Card >
          <Card.Header as="h5" >Ordering</Card.Header>
          <Card.Body>
            <Form>
              <Form.Group as={Row} id="eventOrder">
              <Col md="12">
                  <Form.Label><b>Sort by</b></Form.Label>
                </Col>
                <Col md="12">
                  <Form.Check inline defaultChecked={sortOrder === 2} name="order" label="Date" type="radio" id="orderDate" value="date" onChange={this.onOrderChange} />
                  <Form.Check inline name="order" label="Status" type="radio" id="orderStatus" value="status" onChange={this.onOrderChange} />
                  <Form.Check inline name="order" label="Category" type="radio" id="orderCategory" value="closed" onChange={this.onOrderChange} />
                </Col>
              </Form.Group>
              <Form.Group as={Row} id="eventOrderDirection">
              <Col md="12">
                  <Form.Label><b>Sort direction</b></Form.Label>
                </Col>
                <Col md="12">
                  <Form.Check inline name="orderDirection" label="Ascending" type="radio" id="orderDirectionAsc" value="asc" onChange={this.onOrderDirectionChange} />
                  <Form.Check inline defaultChecked={sortType === 2} name="orderDirection" label="Descending" type="radio" id="orderDirectionDescending" value="desc" onChange={this.onOrderDirectionChange} />                  
                </Col>
              </Form.Group>
            </Form>
          </Card.Body>
        </Card>
        <Card >
          <Card.Header as="h5" >Filter</Card.Header>
          <Card.Body>
            <Form>
              <Form.Group as={Row} id="eventStatusFilter">
                <Col md="12">
                  <Form.Label><b>Status</b></Form.Label>
                </Col>
                <Col md="12">
                  <Form.Check inline defaultChecked={filterStatus === 'all'} name="status" label="Any" type="radio" id="statusAll" value="all" onChange={this.onStatusChange} />
                  <Form.Check inline name="status" label="Open" type="radio" id="statusOpen" value="open" onChange={this.onStatusChange} />
                  <Form.Check inline name="status" label="Closed" value="closed" id="statusClosed" type="radio" onChange={this.onStatusChange} />
                </Col>
              </Form.Group>
              <Form.Group as={Row} id="eventDateFilter">
                <Col md="12">
                  <Form.Label><b>Date</b></Form.Label>
                </Col>
                <Col md="12">
                  <Form.Control type="date" name="dateFrom" onChange={this.onDateChange} />
                  <Form.Control type="date" name="dateTo" onChange={this.onDateChange} />
                </Col>
              </Form.Group>

              <Form.Group as={Row} id="eventCategoryFilter">
                <Col md="12">
                  <Form.Label><b>Category</b></Form.Label>
                </Col>
                <Col md="12">
                  {eventCategories && eventCategories.map(eventCategory =>

                    <Form.Check key={eventCategory.id} type="checkbox" value={eventCategory.id}
                      defaultChecked={eventCategory.selected} label={eventCategory.title} onChange={this.onCategorySelected} />

                  )}
                </Col>
              </Form.Group>
            </Form>
          </Card.Body>
        </Card>
      </div>
    )
  }
}
export default connect(mapStateToProps, mapDispatcherToProps)(SearchBar); 