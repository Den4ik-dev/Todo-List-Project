import React from 'react';
import ReactDOM from 'react-dom/client';
import Router from './components/screens/ui/Router';
import AuthProvider from './providers/AuthProvider';
import './global.css';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>
      <Router />
    </AuthProvider>
  </React.StrictMode>
);
