import Data from  './Data'
const baseUrl = 'topic'

class TopicData {
  static getReplies(id, page) {
    return Data.get(`${baseUrl}/replies?id=${id}&page=${page}`, true)
  }

  static getInfo(id) {
    return Data.get(`${baseUrl}/info?id=${id}`, true)
  }

  static addTopic(topic) {
    return Data.post(`${baseUrl}/create`, topic, true)
  }
}

export default TopicData