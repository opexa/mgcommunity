import React from 'react'
import Topic from './Topic'
import categoryActions from '../../actions/CategoryActions'
import categoryStore from '../../stores/CategoryStore'

class FeedPage extends React.Component {
  constructor (props) {
    super(props)

    this.state = {
      category: {},
      categoryId: this.props.match.params.id,
      topics: [],
      page: 1
    }

    this.handleTopicsFetched = this.handleTopicsFetched.bind(this)

    categoryStore.on(categoryStore.eventTypes.CATEGORIES_FETCHED, this.handleTopicsFetched)
  }

  componentWillUnmount () {
    categoryStore.removeListener(categoryStore.eventTypes.CATEGORIES_FETCHED, this.handleTopicsFetched)
  }

  componentWillMount () {
    categoryActions.getFeed({
      page: this.state.page,
      categoryId: this.state.categoryId
    })
  }

  handleTopicsFetched (data) {
    let newState = {
      topics: data.topics
    }
    if (this.state.page === 1)
      newState.category = data.category

    this.setState(newState)
  }

  render () {
    return (
      <div className='feed-container'>
        <div className="category-name">
          <h2>{this.state.category.name}</h2>
          <p className='category-description'>{this.state.category.description}</p>
        </div>
        <div className="category-topics">
          {this.state.topics.map(topic => 
            <Topic topic={topic} key={topic.id}/>
          )}
        </div>
      </div>
    )
  }
}

export default FeedPage