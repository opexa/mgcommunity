import Data from  './Data'
const baseUrl = 'topic'

class TopicData {
  static getReplies(params) {
    return Data.get(`${baseUrl}/replies?id=${params.id}&page=${params.page}`, true)
  }

  static getInfo(id) {
    return Data.get(`${baseUrl}/info?id=${id}`, true)
  }
}

export default TopicData