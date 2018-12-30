import React, { Component } from 'react'
import Navbar from './components/common/Navbar'
import Footer from './components/common/Footer'
import Routes from './components/common/routes/Routes'

import './public/css/index.css'

class App extends Component {
  render() {
    return (
      <div className='app'>
        <Navbar />
        <div className='container page-container'>
          <Routes/>
        </div>
        <Footer />
      </div>
    );
  }
}

export default App
