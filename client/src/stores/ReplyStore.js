import { EventEmitter } from 'events'
import ReplyData from '../data/ReplyData'
import replyActions from '../actions/ReplyActions'
import dispatcher from '../dispatcher';

class ReplyStore extends EventEmitter {
  handleAction (action) {
    switch (action.type) {
      case replyActions.types.ADD_REPLY: {
        this.addReply(action.data)
        break
      }
      default: break
    }
  }
  
  addReply (content) {
    ReplyData
      .addReply(content)
      .then(data => this.emit(this.eventTypes.REPLY_ADDED, data))
  }
}

let replyStore = new ReplyStore()
replyStore.eventTypes = {
  REPLY_ADDED: 'reply_added'
}

dispatcher.register(replyStore.handleAction.bind(replyStore))

export default replyStore