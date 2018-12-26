import React from 'react'
import { Link } from 'react-router-dom'
import Auth from '../account/Auth'
import homeActions from '../../actions/HomeActions'
import homeStore from '../../stores/HomeStore'

class HomePage extends React.Component { 
  constructor (props) {
    super(props)

    this.state = {
      sections: []
    }

    this.handleSectionsFetched = this.handleSectionsFetched.bind(this)
  
    homeStore.on(homeStore.eventTypes.SECTIONS_FETCHED, this.handleSectionsFetched)
  }

  componentWillUnmount () {
    homeStore.removeListener(homeStore.eventTypes.SECTIONS_FETCHED, this.handleSectionsFetched)    
  }

  componentWillMount () {
    if (!Auth.isUserAuthenticated())
      return this.props.history.push('/account/login')

    homeActions.getSections()
  }
  
  handleSectionsFetched (sections) {
    this.setState({ sections })
  }

  render () {
    return  (
      <div className="row">
        <div class='page-container col-md-12'>
          <div className='page-actions'>
            <div className="page-header">
              <h2>Списък</h2>
            </div>
            <div class='create-topic-btn'>
              <i className="fas fa-pen"></i>&nbsp;
              НОВА ТЕМА
            </div>
          </div>
          <div className="row">
            <div className='sections-container col-md-10'>
              {this.state.sections.map(section => (
                <div className='section' key={section.id}>
                  <div className='section-name'>{section.name.toUpperCase()}</div> 
                  <div className='categories-container'>
                    {section.categories.map(category => (
                      <div className='category' key={category.id}>
                        <div className='category-name'>
                          <Link to={`/category/feed/${category.id}`}>{category.name}</Link>
                        </div>
                        <div className='category-actions'>
                          <span className='topics-count'>{category.topicsCount} теми</span>
                          {/* LAST REPLY */}
                          {/* REPLIES COUNT */}
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    )
  }
}

export default HomePage
