import React from 'react'
import Input from '../../common/forms/Input'

const RegisterForm = (props) => (
  <form className='col-md-5'>
    <div className='form-group'>
      <fieldset>
        <legend>Включи се в общността</legend>
        <hr/>
        {/* { (props.error !== '') ? (
          <div className='form-error'>{props.error}</div>
        ) : (
          <span></span>
        )} */}
        <Input
          name='firstName'
          placeholder='Собствено име'
          value={props.user.username}
          onChange={props.onChange} />
        <Input
          name='lastName'
          placeholder='Фамилно име'
          value={props.user.lastName}
          onChange={props.onChange} />
        <Input
          name='username'
          placeholder='Потребителско име'
          value={props.user.username}
          onChange={props.onChange} />
        <Input 
          type='email'
          name='email'
          placeholder='Имейл'
          value={props.user.email}
          onChange={props.onChange} />
        <Input 
          type='number'
          name='startYear'
          placeholder='Първа година в МГ'
          value={props.user.startYear}
          onChange={props.onChange} />
        <div className='form-group'>
          <label htmlFor='class'>Буква на паралелка</label>
          <select name='class' id='class' className='form-control' value={props.user.class} onChange={props.onChange}>
            <option value='0'>А</option>
            <option value='1'>Б</option>
            <option value='2'>В</option>
            <option value='3'>Г</option>
            <option value='4'>Д</option>
            <option value='5'>Е</option>
            <option value='6'>Ж</option>
            <option value='7'>З</option>
          </select>
        </div>
        <Input 
          type='password'
          name='password'
          placeholder='Парола'
          value={props.user.password}
          onChange={props.onChange} />
        <Input 
          type='password'
          name='confirmPassword'
          placeholder='Потвърди паролата'
          value={props.user.confirmPassword}
          onChange={props.onChange} />
        <input type='submit' value='Регистрирай се' onClick={props.onSubmit} className='btn btn-primary btn-lg btn-block'/>
      </fieldset>
    </div>
  </form>
)

export default RegisterForm
