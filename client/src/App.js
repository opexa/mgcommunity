import React, { Component } from 'react'
import Navbar from './components/common/Navbar'
import Routes from './components/common/routes/Routes'

class App extends Component {
  render() {
    return (
      <div className='App'>
        <Navbar />
        <div className='container'>
          <Routes/>
        </div>
      </div>
    );
  }
}

export default App
