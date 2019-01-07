import { EventEmitter } from 'events'
import dispatcher from '../dispatcher'
import categoryActions from '../actions/CategoryActions'
import CategoryData from '../data/CategoryData'

class CategoryStore extends EventEmitter {
  getFeed (categoryId, page) {
    CategoryData
      .getFeed(categoryId, page)
      .then(data => this.emit(this.eventTypes.CATEGORIES_FETCHED, data))
  }

  getName (id) {
    CategoryData
      .getName(id)
      .then(data => this.emit(this.eventTypes.CATEGORY_NAME_FETCHED, data))
  }

  handleAction (action) {
    switch (action.type) {
      case categoryActions.types.GET_FEED: {
        this.getFeed(action.params.categoryId, action.params.page)
        break
      }
      case categoryActions.types.GET_NAME: {
        this.getName(action.id)
        break
      }
      default: break
    }
  }
}

let categoryStore = new CategoryStore()
categoryStore.eventTypes = {
  CATEGORIES_FETCHED: 'categories_fetched',
  CATEGORY_NAME_FETCHED: 'category_name_fetched'
}

dispatcher.register(categoryStore.handleAction.bind(categoryStore))

export default categoryStore
