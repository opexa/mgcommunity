import React from 'react'
import Topic from './Topic'
import { Link } from 'react-router-dom'
import categoryActions from '../../actions/CategoryActions'
import categoryStore from '../../stores/CategoryStore'

class FeedPage extends React.Component {
  constructor (props) {
    super(props)

    this.validateRoute(this.props.match.params)

    this.state = {
      category: {},
      categoryId: this.props.match.params.id,
      sectionId: this.props.match.params.sectionId,
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

  validateRoute (params) {
    if (isNaN(params.id))
      this.props.history.push('/NotFound')
  }

  render () {
    let state = this.state
    return (
      <div className='feed-container'>
        <div className="category-info">
          <div className='category-name'>
            <h2>{state.category.name}</h2>
            <p className='category-description'>{state.category.description}</p>
          </div>
          <div className="add-topic">
            <Link to={`/${state.sectionId}/category/${state.categoryId}/add`}>
              <i className="fas fa-pen"></i> НОВА ТЕМА
            </Link>
          </div>
        </div>
        <div className="category-topics">
          {state.topics.map(topic => 
            <Topic topic={topic} sectionId={state.sectionId} categoryId={state.categoryId} key={topic.id}/>
          )}
        </div>
      </div>
    )
  }
}

export default FeedPage
