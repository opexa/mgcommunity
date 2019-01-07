import Data from './Data'
const baseUrl = 'reply'

class ReplyData {
  static addReply(data) {
    return Data.post(`${baseUrl}/add`, data, true)
  }
}

export default ReplyData