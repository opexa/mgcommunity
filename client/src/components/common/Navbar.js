import React from 'react'
import { Link } from 'react-router-dom'
import Auth from '../account/Auth'

class Navbar extends React.Component {
  render () {
    return (
      <nav className='navbar navbar-expand-lg navbar-dark bg-primary'>
        <div className='container'>
          <Link className='navbar-brand' to='/'>Community</Link>
          { Auth.isUserAuthenticated() ? (
            <ul className='navbar-nav mr-auto'>
              <li className='nav-item dropdown'>
                <Link className='nav-link dropdown-toggle' data-toggle='dropdown' id='progress' role='button' aria-haspopup='true' aria-expanded='false' to='/progress'>
                  MANAGER &nbsp;<i className='fas fa-tasks'></i>&nbsp;
                  <span className='sr-only'>(current)</span>
                </Link>
                <div className='dropdown-menu' aria-labelledby='progress'>
                  <Link className='dropdown-item' to='/spendings/add'>+ &nbsp;Spending</Link>
                  <Link className='dropdown-item' to='/incomes/add'>+ &nbsp;Income</Link>
                  <div className='dropdown-divider'></div>
                  <Link className='dropdown-item' to='/spendings'>Spendings</Link>
                  <Link className='dropdown-item' to='/incomes'>Incomes</Link>
                  <div className='dropdown-divider'></div>
                  <Link className='dropdown-item' to='/progress/history'>History</Link>
                </div>
              </li>
              <li className='nav-item'>
                <Link className='nav-link' to='/strategy/create'>
                  CONFIGURE &nbsp;<i className='fas fa-user-cog'></i>
                  <span className='sr-only'>(current)</span>
                </Link>
              </li>
            </ul>
          ) : (
            <ul className='navbar-nav mr-auto'>
              <li className='nav-item active'>
              </li>
            </ul>
          ) }
          <div /*className='collapse navbar-collapse'*/>
            <ul className='navbar-nav'></ul>
            { Auth.isUserAuthenticated() ? (
              <ul className='nav navbar-nav ml-auto'>
                <li className='nav-item'><Link className='nav-link' to='/profile'>Профил &nbsp; <i className='far fa-user'></i></Link></li>
                <li className='nav-item'><Link className='nav-link' to='/account/logout'>Изход</Link></li>
              </ul>
            ) : (
              <ul className='nav navbar-nav ml-auto'>
                <li className='nav-item'><Link className='nav-link' to='/account/register'>Регистрация</Link></li>
                <li className='nav-item'><Link className='nav-link' to='/account/login'>Вход</Link></li>
              </ul>
            )}
          </div>          
        </div>
      </nav>
    )
  }
}

export default Navbar