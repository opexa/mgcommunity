import { EventEmitter } from 'events'
import feedActions from '../actions/FeedActions'
import FeedData from '../data/FeedData'

class CategoryStore extends EventEmitter {
  getFeed (categoryId, page) {
    FeedData
      .getFeed(categoryId, page)
      .then(data => this.emit(this.eventTypes.CATEGORIES_FETCHED, data))
  }

  handleAction (action) {
    switch (action.type) {
      case feedActions.types.GET_TOPICS: {
        this.getTopics(action.params.categoryId, action.params.page)
        break
      }
      default: break
    }
  }
}

let feedStore = new FeedStore()
feedStore.eventTypes = {
  CATEGORIES_FETCHED: 'categories_fetched'
}