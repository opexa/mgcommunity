import React from 'react'
import AccountMenu from '../AccountMenu'

class AccountPage extends React.Component {
  render () {
    return (
      <div className="account-details row">
        <AccountMenu active='details'/>
        <div className="account-details-content">
          Content
        </div>
      </div>
    )
  }
}

export default AccountPage