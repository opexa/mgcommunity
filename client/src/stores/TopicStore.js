import { EventEmitter } from 'events'
import dispatcher from '../dispatcher'
import topicActions from '../actions/TopicActions'
import TopicData from '../data/TopicData'

class TopicStore extends EventEmitter {
  handleAction (action) {
    switch (action.type) {
      case topicActions.types.GET_REPLIES: {
        this.getReplies(action.params.id, action.params.page)
        break
      }
      case topicActions.types.GET_INFO: {
        this.getInfo(action.id)
        break
      }
      case topicActions.types.ADD_TOPIC: {
        this.addTopic(action.topic)
        break
      }
      default: break
    }
  }

  getReplies (id, page) {
    TopicData
      .getReplies(id, page)
      .then(data => this.emit(this.eventTypes.REPLIES_FETCHED, data))
  }

  getInfo (id) {
    TopicData
      .getInfo(id)
      .then(data => this.emit(this.eventTypes.INFO_FETCHED, data))
  }

  addTopic(topic) {
    TopicData
      .addTopic(topic)
      .then(data => this.emit(this.eventTypes.TOPIC_ADDED, data))
  }
}

let topicStore = new TopicStore()
topicStore.eventTypes = {
  REPLIES_FETCHED: 'replies_fetched',
  INFO_FETCHED: 'info_fetched',
  TOPIC_ADDED: 'topic_added'
}

dispatcher.register(topicStore.handleAction.bind(topicStore))

export default topicStore
