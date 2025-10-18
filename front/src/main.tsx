import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { AppBar } from './components/AppBar.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AppBar />
    <App />
  </StrictMode>,
)
