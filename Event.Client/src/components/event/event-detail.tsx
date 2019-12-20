import React from 'react'
import {  RouteComponentProps } from 'react-router-dom';
import EventsService from '../../services/events-service';
import Event from './event';
import Card from 'react-bootstrap/Card';
import Badge from 'react-bootstrap/Badge';
import { Link } from 'react-router-dom'

interface IState {
}

interface RouteInfo {
  id: string;
}

interface IState {
  event: Event;
}

interface ComponentProps extends RouteComponentProps<RouteInfo> {
}

class EventPage extends React.Component<ComponentProps, IState> {
  private _eventService: EventsService;
  constructor(props: any) {
    super(props);
    this._eventService = new EventsService();
    this.state = { event: new Event() };
  }

  public async componentDidMount(): Promise<void> {
    var eventId = this.props.match.params.id;
    var response = await this._eventService.getEvent(eventId);
    this.setState({ event: response });
  }


  render() {
    var event = this.state.event;
    return (
      <div>
        <Link to="/">Back</Link><br />
        <Card key={event.id}>
          <Card.Header as="h5" className={event.closed !== null ? 'closed-event' : ''}>{event.title}</Card.Header>
          <Card.Body>
            <b>Description</b> <p>{event.description}</p>            
            <p><a href={event.link} target="parent">View on EONET</a></p>
            {event.categories && event.categories.map(category =>
              <Badge key={category.id + '-' + event.id} variant="info">{category.title}</Badge>
            )}
          </Card.Body>
        </Card>
      </div>
    )
  }
}
export default EventPage