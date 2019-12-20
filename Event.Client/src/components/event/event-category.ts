export default class EventCategory {

    constructor()
    {
        this.id = -1;
        this.title = null;
        this.selected = true;
    }

    id: number;
    title: string | null;
    selected: boolean;
}