import React from 'react'
import { Switch, Route } from 'react-router-dom'
import PrivateRoute from './PrivateRoute'

import HomePage from '../HomePage'

const Routes = (props) => (
  <Switch>
    <Route path='/' exact component={HomePage}/>
  </Switch>
)

export default Routes