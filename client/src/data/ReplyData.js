import Data from './Data'
const baseUrl = 'reply'

class ReplyData {
  static addReply(content) {
    return Data.post(`${baseUrl}/add`, content, true)
  }
}

export default ReplyData