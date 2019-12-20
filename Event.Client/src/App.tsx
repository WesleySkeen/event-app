import React from 'react'
import { Route, Switch } from 'react-router-dom'
// We will create these two pages in a moment
import HomePage from './components/index/index'
import EventPage from './components/event/event-detail'

export default function App() {
  return (
    <Switch>
      <Route exact path="/" component={HomePage} />  
      <Route path="/:id" component={EventPage} />    
    </Switch>
  )
}

