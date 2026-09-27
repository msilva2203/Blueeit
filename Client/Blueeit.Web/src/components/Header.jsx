import NavLink from "./NavLink"
import ActionBar from "./ActionBar"

import './Header.css';

export default function Header() {
    return (
        <header className="blueeit-header">
            <div className="blueeit-page-content">
                <h1 className="blueeit-header-logo">Blueeit</h1>
            </div>
            <div className="blueeit-header-internal">
                <div className="blueeit-page-content">
                    <nav className="blueeit-header-nav">
                        <div className="blueeit-header-nav-list">
                            <NavLink href="/forums">Forums</NavLink>
                            <NavLink href="/members">Members</NavLink>
                            <NavLink href="/contact">Contact</NavLink>
                            <NavLink href="/about">About</NavLink>
                        </div>
                    </nav>
                </div>
            </div>
            <ActionBar />
        </header>
    );
}