import EventCategory from "./event-category";

export default class Event {

    constructor()
    {
        this.id = '';
        this.title = null;
        this.description = null;
        this.link = '';
        this.closed = null;
        this.categories = new Array<EventCategory>();
    }

    id: string;
    title: string | null;
    description: | null;
    link: string;
    closed: string| null;
    categories: Array<EventCategory>;
}