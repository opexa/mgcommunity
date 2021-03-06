import React from 'react'
import Auth from '../Auth'
import LoginForm from './LoginForm'
import FormHelpers from '../../common/forms/FormHelpers'
import accountActions from '../../../actions/AccountActions'
import accountStore from '../../../stores/AccountStore'
import iziToast from '../../../../node_modules/izitoast'

class LoginPage extends React.Component {
  constructor(props) {
    super(props)

    this.state = {
      user: {
        username: 'admin',
        password: 'mgadmin1298'
      },
      error: ''
    }

    this.handleUserLoggedIn = this.handleUserLoggedIn.bind(this)

    accountStore.on(accountStore.eventTypes.USER_LOGGED_IN, this.handleUserLoggedIn)
  }

  componentWillUnmount() {
    accountStore.removeListener(accountStore.eventTypes.USER_LOGGED_IN, this.handleUserLoggedIn)
  }

  handleInputChange (event) {
    FormHelpers.handleFormChange.bind(this)(event, 'user')
  }

  handleFormSubmit (event) {
    event.preventDefault()
    
    if (!this.validateLoginForm())
      return iziToast.warning({ message: "Моля въведете потребителско име и парола." })
    
    accountActions.login(this.state.user)
  }

  handleUserLoggedIn (data) {
    if (data.error) {
      return iziToast.error({ message: data.error_description || 'Възникна някаква грешка. Моля опитайте отново.' })
    }
    Auth.authenticateUser(data.access_token)
    Auth.setUser({
      username: data.username,
      role: data.role,
      avatar: data.avatar
    })
    this.props.history.push('/')
  }

  validateLoginForm () {
    let user = this.state.user
    if (user.username === '' || user.password === '') 
      return false
    return true    
  }

  render () {
    return (
      <div className='login-form-container row justify-content-center align-items-center'>
        <LoginForm 
          user={this.state.user}
          onChange={this.handleInputChange.bind(this)} 
          onSubmit={this.handleFormSubmit.bind(this)}
          error={this.state.error} /> 
      </div>
    )
  }
}

export default LoginPage