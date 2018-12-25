import React from 'react'
import Auth from '../account/Auth'

class HomePage extends React.Component { 
  constructor (props) {
    super(props)

  }

  render () {
    return  (
      <div>
        {Auth.isUserAuthenticated() ? 
        (
          <div>
            <h1>Drasti shefe :)</h1>
          </div>
        ) : 
        (
          <h1>Otivai si ili sa registrirai pedal</h1>
        ) }
      </div>
    )
  }
}

export default HomePage
