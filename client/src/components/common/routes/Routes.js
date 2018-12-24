import React from 'react'
import { Switch, Route } from 'react-router-dom'
import PrivateRoute from './PrivateRoute'

import HomePage from '../HomePage'

import LoginPage from '../../account/login/LoginPage'
import RegisterPage from '../../account/register/RegisterPage'
import LogoutPage from '../../account/LogoutPage'

const Routes = (props) => (
  <Switch>
    <Route path='/' exact component={HomePage}/>

    <Route path='/account/login' component={LoginPage} />
    <Route path='/account/register' component={RegisterPage} />
    <Route path='/account/logout' component={LogoutPage} />
  </Switch>
)

export default Routes