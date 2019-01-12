import React from 'react'
import AccountMenu from '../AccountMenu'
import AccountDetails from './AccountDetails'
import accountActions from '../../../actions/AccountActions'
import accountStore from '../../../stores/AccountStore'

class AccountPage extends React.Component {
  constructor (props) {
    super(props)

    this.state = {
      user: {}
    }

    this.handleUserDetailsFetched = this.handleUserDetailsFetched.bind(this)

    accountStore.on(accountStore.eventTypes.USER_DETAILS_FETCHED, this.handleUserDetailsFetched)
  }
  
  componentWillMount() {
    accountActions.details()
  }

  componentWillUnmount() {
    accountStore.removeListener(accountStore.eventTypes.USER_DETAILS_FETCHED, this.handleUserDetailsFetched)
  }
  
  handleUserDetailsFetched (user) {
    this.setState({ user })
  }

  render () {
    return (
      <div className="account-details row">
        <AccountMenu active='details'/>
        <AccountDetails user={this.state.user} />
      </div>
    )
  }
}

export default AccountPage