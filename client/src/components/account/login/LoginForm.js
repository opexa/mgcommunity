import React from 'react'
import Input from '../../common/forms/Input'

const LoginForm = (props) => (
  <form className='col-md-5 col-md-offset-'>
    <div className='form-group'>
      <fieldset>
        <legend>Вход към платформата</legend>
        <hr/>
        <span className='form-error'>{props.error}</span>
        <Input 
          name='username'
          placeholder='Потребителско име'
          value={props.user.username}
          onChange={props.onChange} />
        <Input 
          type='password'
          name='password'
          placeholder='Парола'
          value={props.user.password}
          onChange={props.onChange} />
        <input type='submit' value='ВЛЕЗ' onClick={props.onSubmit} className='btn btn-primary btn-lg btn-block'/>
      </fieldset>
    </div>
  </form>
)

export default LoginForm
