import * as React from 'react';
import { IRootState } from '../../store';
import { Dispatch } from 'redux';
import { AppActions } from '../../store/types';
import { connect } from 'react-redux';
import Card from 'react-bootstrap/Card';
import Badge from 'react-bootstrap/Badge';
import { Link } from 'react-router-dom'
import './events.css';

const mapStateToProps = ({ app }: IRootState) => {
  const { events } = app;
  return { events };
}

type ReduxType = ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatcherToProps>;

const mapDispatcherToProps = (dispatch: Dispatch<AppActions>) => { return {} }

interface IState {
}

class Events extends React.Component<ReduxType, IState> {

  render() {

    const { events } = this.props;

    return (
      <div className="events">

        {events && events.map(event =>
          <Card key={event.id}>
            <Card.Header as="h5" className={event.closed !== null ? 'closed-event' : ''}>{event.title}</Card.Header>
            <Card.Body>
            <Link to={event.id}>View</Link><br/>
              {event.categories && event.categories.map(category =>
                <Badge key={category.id + '-' + event.id} variant="info">{category.title}</Badge>
              )}
            </Card.Body>
          </Card>
        )}
      </div>
    )
  }
}

export default connect(mapStateToProps, mapDispatcherToProps)(Events);
