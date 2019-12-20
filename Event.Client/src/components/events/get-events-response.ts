import Event from '../event/event'
export default class GetEventsResponse {
    constructor(){
        this.events = new Array<Event>();
        this.pageCount = -1;
    }

    events: Array<Event>;
    pageCount: number;
}