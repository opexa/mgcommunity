import React from 'react'
import { Link } from 'react-router-dom'
import Auth from './Auth'

const AccountMenu = (props) => (
  <div className="account-menu">
    <div className="account-menu-heading">
      Меню
    </div>
    <div className={`account-menu-option ${props.active === 'details' ? 'active' : ''}`}>
      <Link to={`/profile/${Auth.getUser().username}`}>Детайли</Link>
    </div>
    <div className={`account-menu-option ${props.active === 'security' ? 'active' : ''}`}>
      Сигурност
    </div>
    <div className={`account-menu-option ${props.active === 'settings' ? 'active' : ''}`}>
      Настройки
    </div>
    <div className={`account-menu-option ${props.active === 'blocked' ? 'active' : ''}`}>
      Блокирани
    </div>
  </div>
)

export default AccountMenu