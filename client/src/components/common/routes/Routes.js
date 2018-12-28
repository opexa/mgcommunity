import React from 'react'
import { Switch, Route } from 'react-router-dom'
import PrivateRoute from './PrivateRoute'

import HomePage from '../../home/HomePage'

import LoginPage from '../../account/login/LoginPage'
import RegisterPage from '../../account/register/RegisterPage'
import LogoutPage from '../../account/LogoutPage'

import FeedPage from '../../category/FeedPage'

import TopicPage from '../../topic/TopicPage'

const Routes = (props) => (
  <Switch>
    <Route path='/' exact component={HomePage}/>

    <Route path='/account/login' component={LoginPage} />
    <Route path='/account/register' component={RegisterPage} />
    <PrivateRoute path='/account/logout' component={LogoutPage} />

    <PrivateRoute path='/category/feed/:id' component={FeedPage} />

    <PrivateRoute path='/topic/:id' component={TopicPage} />
  </Switch>
)

export default Routes