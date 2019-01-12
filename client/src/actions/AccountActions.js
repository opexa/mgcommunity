import dispatcher from '../dispatcher'

const AccountActions = {
  types: {
    REGISTER_USER: 'REGISTER_USER',
    LOGIN_USER: 'LOGIN_USER',
    USER_DETAILS: 'USER_DETAILS'
  },
  register (user) {
    dispatcher.dispatch({
      type: this.types.REGISTER_USER,
      user
    })
  },
  login (user) {
    dispatcher.dispatch({
      type: this.types.LOGIN_USER,
      user
    })
  },
  details () {
    dispatcher.dispatch({
      type: this.types.USER_DETAILS
    })
  }
}

export default AccountActions