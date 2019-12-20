import Event from '../components/event/event'
import { ActionType } from 'typesafe-actions';
import * as actions from './actions';
import { Filter, SortBy } from '../components/events/get-events-request';
import EventCategory from '../components/event/event-category';

export interface IAppState {
    events: Array<Event>,
    eventCategories: Array<EventCategory>,
    filter: Filter,
    sort: SortBy,
    pageCount: number
}

export type AppActions = ActionType<typeof actions>;


export enum Constants {
    SET_EVENTS = 'SET_EVENTS',
    SET_EVENT_CATEGORIES = 'SET_EVENT_CATEGORIES',
    FILTER_EVENTS = 'FILTER_EVENTS',
    SORT_EVENTS = 'SORT_EVENTS',
    SET_PAGE_COUNT = 'SET_PAGE_COUNT'
}