import React from 'react'
import { Switch, Route, withRouter } from 'react-router-dom'
import PrivateRoute from './PrivateRoute'

import HomePage from '../../home/HomePage'
import NotFoundPage from './NotFoundPage'

import LoginPage from '../../account/login/LoginPage'
import RegisterPage from '../../account/register/RegisterPage'
import LogoutPage from '../../account/LogoutPage'

import FeedPage from '../../category/FeedPage'

import TopicPage from '../../topic/feed/TopicPage'
import AddTopicPage from '../../topic/add/AddTopicPage'

import AccountPage from '../../account/home/AccountPage'

const Routes = (props) => (
  <Switch>
    <Route path='/' exact component={HomePage}/>

    <Route path='/account/login' component={LoginPage} />
    <Route path='/account/register' component={RegisterPage} />
    <PrivateRoute path='/account/logout' component={LogoutPage} />

    <PrivateRoute path='/:sectionId/category/:categoryId/topic/:id/:page?' component={withRouter(TopicPage)} />
    <PrivateRoute path='/:sectionId/category/:id/add' component={AddTopicPage} />
    <PrivateRoute path='/:sectionId/category/:id' component={FeedPage} />
    
    <PrivateRoute path='/account' exact component={AccountPage} />

    <Route path='/' component={NotFoundPage} />
  </Switch>
)

export default Routes