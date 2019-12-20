
import Event  from '../components/event/event'
import EventCategory from '../components/event/event-category'
import { GetEventRequest } from '../components/events/get-events-request'
import HttpService from './http-service'
import GetEventsResponse from '../components/events/get-events-response'

export default class EventsService {
   
    private _httpService : HttpService

    constructor () {
        this._httpService = new HttpService();
    }

    public async getEvents(request: GetEventRequest) : Promise<GetEventsResponse>
    {                
        var response =  await this._httpService.post(request);        
        return response; 
    }

    public async getEventCategories() : Promise<Array<EventCategory>>
    {                
        var response =  await this._httpService.get('Events/GetEventCategories');        
        return response; 
    }

    public async getEvent(id: string) : Promise<Event>
    {
       var response =  await this._httpService.get('Events/'+ id);        
       return response; 
    }
    
 }