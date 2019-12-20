import { action } from 'typesafe-actions';
import { Constants } from './types';
import Event from '../components/event/event'
import { Filter, SortBy } from '../components/events/get-events-request'
import EventCategory from '../components/event/event-category';

export function setEvents(events: Array<Event>) {
    return action(Constants.SET_EVENTS, {
        events
    });
}

export function setEventCategories(eventCategories: Array<EventCategory>) {
    return action(Constants.SET_EVENT_CATEGORIES, {
        eventCategories
    });
}

export function filterEvents(filter: Filter) {
    return action(Constants.FILTER_EVENTS, {
        filter
    });
}

export function sortEvents(sort: SortBy) {
    return action(Constants.SORT_EVENTS, {
        sort
    });
}

export function setPageCount(pageCount: number) {
    return action(Constants.SET_PAGE_COUNT, {
        pageCount
    });
}


