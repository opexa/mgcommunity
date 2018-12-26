import React from 'react'
import categoryActions from '../../actions/CategoryActions'

class FeedPage extends React.Component {
  constructor (props) {
    super(props)

    this.state = {
      category: 'Category Name',
      categoryId: this.props.match.params.id,
      topic: [],
      page: 1
    }
  }

  componentWillMount () {
    categoryActions.getFeed({
      page: this.state.page,
      categoryId: this.state.categoryId
    })
  }

  render () {
    return (
      <div>
        <h1>{this.state.category}</h1>
      </div>
    )
  }
}

export default FeedPage
