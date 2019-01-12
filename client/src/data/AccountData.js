import Data from './Data'
const baseUrl = 'account'

class AccountData {
  static register (user) {
    return Data.post(`${baseUrl}/register`, user, false)
  }

  static login(user) {
    return Data.post(`${baseUrl}/login`, user, false)
  }

  static details() {
    return Data.get(`${baseUrl}/details`, true)
  }
}

export default AccountData