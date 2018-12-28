import { EventEmitter } from 'events'
import dispatcher from '../dispatcher'
import topicActions from '../actions/TopicActions'
import TopicData from '../data/TopicData'

class TopicStore extends EventEmitter {
  handleAction (action) {
    switch (action.type) {
      case topicActions.types.GET_REPLIES: {
        this.getReplies(action.params)
        break
      }
      case topicActions.types.GET_INFO: {
        this.getInfo(action.id)
        break
      }
      default: break
    }
  }

  getReplies (params) {
    TopicData
      .getReplies(params)
      .then(data => this.emit(this.eventTypes.REPLIES_FETCHED, data))
  }

  getInfo (id) {
    TopicData
      .getInfo(id)
      .then(data => this.emit(this.eventTypes.INFO_FETCHED, data))
  }
}

let topicStore = new TopicStore()
topicStore.eventTypes = {
  REPLIES_FETCHED: 'replies_fetched',
  INFO_FETCHED: 'info_fetched'
}

dispatcher.register(topicStore.handleAction.bind(topicStore))

export default topicStore
