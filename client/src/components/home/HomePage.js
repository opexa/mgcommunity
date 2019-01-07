import React from 'react'
import Section from './Section'
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
        <div className='page-container col-md-12'>
          <div className='page-actions'>
            <div className="page-header">
              <h2>Списък</h2>
            </div>
            <div className='create-topic-btn'>
              <i className="fas fa-pen"></i> НОВА ТЕМА
            </div>
          </div>
          <div className="row">
            <div className='sections-container col-md-10'>
              {this.state.sections.map((section, index) => (
                <Section key={index} section={section}/>
              ))}
            </div>
          </div>
        </div>
      </div>
    )
  }
}

export default HomePage
