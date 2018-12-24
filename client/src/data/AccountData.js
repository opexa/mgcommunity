import Data from './Data'
const baseUrl = 'account'

class AccountData {
  static register (user) {
    return Data.post(`${baseUrl}/register`, user, false)
  }

  static login(user) {
    return Data.post(`${baseUrl}/login`, user, false)
  }
}

export default AccountData