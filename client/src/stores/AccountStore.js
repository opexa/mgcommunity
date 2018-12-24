import { EventEmitter } from 'events'
import dispatcher from '../dispatcher'
import accountActions from '../actions/AccountActions'
import AccountData from '../data/AccountData'

class AccountStore extends EventEmitter {
  register (user) {
    AccountData
      .register(user)
      .then(data => this.emit(this.eventTypes.USER_REGISTERED, data))
  }

  login (user) {
    AccountData
      .login(user)
      .then(data => this.emit(this.eventTypes.USER_LOGGED_IN, data))
  }

  handleAction (action) {
    switch (action.type) {
      case accountActions.types.REGISTER_USER: {
        this.register(action.user)
        break
      }
      case accountActions.types.LOGIN_USER: {
        this.login(action.user)
        break
      }
      default: break
    }
  }
}

let accountStore = new AccountStore()

accountStore.eventTypes = {
  USER_REGISTERED: 'user_registerd',
  USER_LOGGED_IN: 'user_logged_in'
}

dispatcher.register(accountStore.handleAction.bind(accountStore))

export default accountStore
