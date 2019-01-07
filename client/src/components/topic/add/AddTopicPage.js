import React from 'react'
import FormHelpers from '../../common/forms/FormHelpers'
import categoryActions from '../../../actions/CategoryActions'
import categoryStore from '../../../stores/CategoryStore'
import AddTopicForm from './AddTopicForm'

import '../../../public/css/add-topic.css'

class AddTopicPage extends React.Component {
  constructor(props) {
    super(props)

    this.state = {
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

  handleNameFetched (data) {
    this.setState({
      category: { 
        name: data.name,
        section: data.section
      }
    })
  }

  render () {
    return (
      <div>
        <div className="post-category-info">
          <h1 className='post-topic-heading'>Публикувай тема</h1>
          <p className='category-name'>
            <i className="fas fa-home"></i>&nbsp;&nbsp;
            <i className="fas fa-angle-double-right"></i>&nbsp;&nbsp;{this.state.category.section}&nbsp;&nbsp;
            <i className="fas fa-angle-right"></i>&nbsp;&nbsp;{this.state.category.name}
          </p>
        </div>
        <div className="post-content">
          <AddTopicForm topic={this.state.topic} onChange={this.handleInputChange.bind(this)} />
        </div>
      </div>
    )
  }
}

export default AddTopicPage