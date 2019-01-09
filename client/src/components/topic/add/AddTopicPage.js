import React from 'react'
import { Link } from 'react-router-dom'
import { HashLink } from "react-router-hash-link"
import iziToast from 'izitoast'
import FormHelpers from '../../common/forms/FormHelpers'
import categoryActions from '../../../actions/CategoryActions'
import categoryStore from '../../../stores/CategoryStore'
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
        title: '',
        content: ''
      }
    }

    this.handleNameFetched = this.handleNameFetched.bind(this)

    categoryStore.on(categoryStore.eventTypes.CATEGORY_NAME_FETCHED, this.handleNameFetched)
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
      alert ()
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
    if (post.content.length < 20) {
      isFormValid = false
      iziToast.warning({ message: 'Съдържанието на темата трябва да поне 20 символа дълго.'})
    }
    return isFormValid
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

  componentDidMount() {
    window.location.hash = window.decodeURIComponent(window.location.hash);
    const scrollToAnchor = () => {
      const hashParts = window.location.hash.split('#');
      if (hashParts.length > 2) {
        const hash = hashParts.slice(-1)[0];
        document.querySelector(`#${hash}`).scrollIntoView();
      }
    };
    scrollToAnchor();
    window.onhashchange = scrollToAnchor;
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