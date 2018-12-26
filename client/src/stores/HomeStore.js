import { EventEmitter } from 'events'
import dispatcher from '../dispatcher'
import homeActions from '../actions/HomeActions'
import homeData from '../data/HomeData'

class HomeStore extends EventEmitter {
  getSections () {
    homeData
      .getSections()
      .then(data => this.emit(this.eventTypes.SECTIONS_FETCHED, data))
  }

  handleAction (action) {
    switch (action.type) {
      case homeActions.types.GET_SECTIONS: {
        this.getSections()
        break
      }
      default: break
    }
  }
}

let homeStore = new HomeStore()
homeStore.eventTypes = {
  SECTIONS_FETCHED: 'sections_fetched'
}

dispatcher.register(homeStore.handleAction.bind(homeStore))

export default homeStore