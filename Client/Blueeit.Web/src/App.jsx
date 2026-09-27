import { BrowserRouter, Routes, Route } from "react-router-dom";

import Header from './components/Header';
import Footer from "./components/Footer";

import ForumsPage from "./pages/ForumsPage"
import ForumPage from "./pages/ForumPage"
import ThreadPage from "./pages/ThreadPage"
import MembersPage from "./pages/MembersPage"
import ContactPage from "./pages/ContactPage"
import AboutPage from "./pages/AboutPage"

import './App.css';
import MemberPage from "./pages/MemberPage";

function App() {
    return (
        <BrowserRouter>
            <div className="app">
                <Header />
                <main>
                  <div className="blueeit-page-content">
                    <div className="blueeit-main-page">
                      <Routes>
                        <Route path="/forums" element={<ForumsPage />} />
                        <Route path="/forums/:id" element={<ForumPage />} />
                        <Route path="/forums/threads/:id" element={<ThreadPage />} />
                        <Route path="/members" element={<MembersPage />} />
                        <Route path="/members/:id" element={<MemberPage />} />
                        <Route path="/contact" element={<ContactPage />} />
                        <Route path="/about" element={<AboutPage />} />
                      </Routes>
                    </div>
                  </div>
                </main>
                <Footer />
            </div>
        </BrowserRouter>
    );
}

export default App
