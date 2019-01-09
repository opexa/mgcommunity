import React from 'react'
import { Link } from 'react-router-dom'
import { HashLink } from "react-router-hash-link"
import iziToast from 'izitoast'
import FormHelpers from '../../common/forms/FormHelpers'
import categoryActions from '../../../actions/CategoryActions'
import categoryStore from '../../../stores/CategoryStore'
import topicActions from '../../../actions/TopicActions'
import topicStore from '../../../stores/TopicStore'
import AddTopicForm from './AddTopicForm'

import '../../../public/css/add-topic.css'

class AddTopicPage extends React.Component {
  constructor(props) {
    super(props)

    this.state = {
      sectionId: this.props.match.params.sectionId,
      category: {
        name: '',
        section: '',
        id: this.props.match.params.id
      },
      topic: {
        title: 'Нова тема нова тема',
        firstReply: 'Нека пробваме дали работи пък'
      }
    }

    this.handleNameFetched = this.handleNameFetched.bind(this)
    this.handleTopicAdded = this.handleTopicAdded.bind(this)

    categoryStore.on(categoryStore.eventTypes.CATEGORY_NAME_FETCHED, this.handleNameFetched)
    topicStore.on(topicStore.eventTypes.TOPIC_ADDED, this.handleTopicAdded)
  }

  componentWillUnmount () {
    categoryStore.removeListener(categoryStore.eventTypes.CATEGORY_NAME_FETCHED, this.handleNameFetched)
    topicStore.removeListener(topicStore.eventTypes.TOPIC_ADDED, this.handleTopicAdded)
  }

  componentWillMount () {
    categoryActions.getName(this.state.category.id)
  }

  handleInputChange (ev) {
    FormHelpers.handleFormChange.bind(this)(ev, 'topic')
  }

  handleFormSubmit (ev) {
    ev.preventDefault()

    if (this.validateForm()) {
      let data = this.state.topic
      data.categoryId = this.state.category.id
      topicActions.addTopic(data)
    }
  }

  validateForm () {
    iziToast.destroy()
    let post = this.state.topic
    let isFormValid = true

    if (post.title.length < 5)  {
      isFormValid = false
      iziToast.warning({ message: 'Заглавието трябва да е съдържа поне 5 символа.'})
    }
    if (post.firstReply.length < 20) {
      isFormValid = false
      iziToast.warning({ message: 'Съдържанието на темата трябва да поне 20 символа дълго.'})
    }
    return isFormValid
  }

  handleTopicAdded (data) {
    if (data.added) {
      iziToast.success({ message: data.message })
      this.props.history.push(`/${this.state.sectionId}/category/${this.state.category.id}`)
    }
  }

  handleNameFetched (data) {
    this.setState(prevState => ({
      category: { 
        name: data.name,
        section: data.section,
        id: prevState.category.id
      }
    }))
  }

  render () {
    let state = this.state

    return (
      <div>
        <div className="post-category-info">
          <h1 className='post-topic-heading'>Публикувай тема</h1>
          <p className='category-name'>
            <i className="fas fa-home"></i>&nbsp;&nbsp;
            <i className="fas fa-angle-double-right"></i>&nbsp;&nbsp;<HashLink to={`/#${state.sectionId}`}>{state.category.section}</HashLink>&nbsp;&nbsp;
            <i className="fas fa-angle-right"></i>&nbsp;&nbsp;<Link to={`/${state.sectionId}/category/${state.category.id}`}>{state.category.name}</Link>
          </p>
        </div>
        <div className="post-content">
          <AddTopicForm topic={state.topic} onChange={this.handleInputChange.bind(this)}
                                                onSubmit={this.handleFormSubmit.bind(this)} />
        </div>
      </div>
    )
  }
}

export default AddTopicPage