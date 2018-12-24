import React from 'react'
import Auth from '../Auth'
import FormHelpers from '../../common/forms/FormHelpers'
import RegisterForm from './RegisterForm'
import accountActions from '../../../actions/AccountActions'
import accountStore from '../../../stores/AccountStore'
import izitoast from 'izitoast'

class RegisterPage extends React.Component {
  constructor (props) {
    super(props) 

    this.state = {
      user: {
        username: 'test',
        email: 'test@test.bg',
        firstName: 'Test',
        lastName: 'Testov',
        startYear: 2012,
        class: 'G',
        password: '123456',
        confirmPassword: '123456'
      }
    }

    this.handleUserRegistered = this.handleUserRegistered.bind(this)

    accountStore.on(accountStore.eventTypes.USER_REGISTERED, this.handleUserRegistered)
  }

  componentWillUnmount () {
    accountStore.removeListener(
      accountStore.eventTypes.USER_REGISTERED,
      this.handleUserRegistered
    )
  }

  handleUserRegistered (data) {
    if (!data.modelState.length === 0) {
      Auth.authenticateUser(data.access_token)
      this.props.history.push('/')
    } else {
      this.showModelErrors(data.modelState)
    }
  }

  handleInputChange (event) {
    FormHelpers.handleFormChange.bind(this)(event, 'user');
  }

  handleFormSubmit (event) {
    event.preventDefault();

    if (this.validateForm()) {
      accountActions.register(this.state.user)
    }
  }

  validateForm () {
    let user = this.state.user
    let isFormValid = true
    let error = ''

    if (user.username === '' || user.email === '' || user.password === '' || user.confirmPassword === '' || user.firstName === '' || user.lastName === '') {
      isFormValid = false
      error = 'Моля, попълнете всички полета.'
    }

    if (user.password !== user.confirmPassword) {
      isFormValid = false
      error = 'Паролите не съвпадат.'
    }
    
    this.setState({ error })
    return isFormValid
  }

  showModelErrors (model) {
    if (Object.keys(model).length > 0) {
      Object.keys(model).map((key) => {
        izitoast.error({ message: model[key][0] })
      })
    } else {
      izitoast.error({ message: 'Възникна някаква грешка в сървъра. Моля опитайте отново.' })
    }
  }

  render () {
    return (
      <div className='container'>
        <div className='register-form-container row justify-content-center align-items-center'>
          <RegisterForm 
            user={this.state.user}
            onChange={this.handleInputChange.bind(this)} 
            onSubmit={this.handleFormSubmit.bind(this)}
            error={this.state.error} /> 
        </div>
      </div>
    )
  }
}

export default RegisterPage
