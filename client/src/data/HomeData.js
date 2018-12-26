import Data from './Data'
const baseUrl = 'home'

class HomeData {
  static getSections() {
    return Data.get(`${baseUrl}/getSections`, true)
  }
}

export default HomeData