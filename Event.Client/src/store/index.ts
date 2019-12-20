import { combineReducers, createStore } from 'redux';
import { appReducer } from './reducer';
import { IAppState } from './types';
import 'bootstrap/dist/css/bootstrap.min.css';

export interface IRootState {
    app: IAppState
}
const store = createStore<IRootState, any, any, any>(
    combineReducers({
        app: appReducer
}));

export default store;