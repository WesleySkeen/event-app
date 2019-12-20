import axios from 'axios';
export default class HttpService {
   
    public async post(payload : any) : Promise<any>
    {        
        let result = await axios.post('http://localhost:5000/api/1.0/Events', payload);
        if(result.status !== 200)
            console.log(result);

        return result.data;     
    }

    public async get(route: string) : Promise<any>
    {        
        let result = await axios.get('http://localhost:5000/api/1.0/' + route);
        if(result.status !== 200)
            console.log(result);

        return result.data;     
    }
 }