import Event from '../components/event/event'
import { Constants, AppActions, IAppState } from './types';
import { Filter, SortBy } from '../components/events/get-events-request'
import EventCategory from '../components/event/event-category';

const init: IAppState = {
    events: Array<Event>(),
    filter: new Filter(),
    sort: new SortBy(),
    eventCategories: Array<EventCategory>(),
    pageCount: -1
};

export function appReducer(state: IAppState = init, action: AppActions): IAppState {
    switch (action.type) {
        case Constants.SET_EVENTS:
            return {             
                eventCategories: state.eventCategories,    
                events: state.events = action.payload.events,
                sort: state.sort,
                filter: state.filter,
                pageCount: state.pageCount
            };
        case Constants.FILTER_EVENTS:
            return {                 
                eventCategories: state.eventCategories,
                filter: state.filter = action.payload.filter,
                sort: state.sort,
                events: state.events,
                pageCount: state.pageCount
            };
        case Constants.SORT_EVENTS:
            return {               
                eventCategories: state.eventCategories,  
                sort: state.sort = action.payload.sort,
                events: state.events,
                filter: state.filter,
                pageCount: state.pageCount
            };
        case Constants.SET_EVENT_CATEGORIES:
            return {          
                eventCategories: state.eventCategories = action.payload.eventCategories,    
                sort: state.sort,
                events: state.events,
                filter: state.filter,
                pageCount: state.pageCount
            };
        case Constants.SET_PAGE_COUNT:
            return {     
                pageCount: state.pageCount = action.payload.pageCount,    
                eventCategories: state.eventCategories,    
                sort: state.sort,
                events: state.events,
                filter: state.filter
            };
        default:
            return state;
    }
}