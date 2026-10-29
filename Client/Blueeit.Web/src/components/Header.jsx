import { useLocation } from "react-router-dom";

import NavLink from "./NavLink";
import ActionBar from "./ActionBar";

import './Header.css';

export default function Header() {
    const location = useLocation();

    const isForums = location.pathname.startsWith("/forums");
    const isWhatsNew = location.pathname.startsWith("/whats-new");
    const isMembers = location.pathname.startsWith("/members");
    const isHelp = location.pathname.startsWith("/help");

    return (
        <header className="blueeit-header">
            <div className="blueeit-page-content">
                <h1 className="blueeit-header-logo">Blueeit</h1>
            </div>
            <div className="blueeit-header-internal">
                <div className="blueeit-page-content">
                    <nav className="blueeit-header-nav">
                        <div className="blueeit-header-nav-list">
                            <NavLink href="/forums" isSelected={isForums}>Forums</NavLink>
                            <NavLink href="/whats-new" isSelected={isWhatsNew}>What's New</NavLink>
                            <NavLink href="/members" isSelected={isMembers}>Members</NavLink>
                            <NavLink href="/help/about" isSelected={isHelp}>Help</NavLink>
                        </div>
                    </nav>
                </div>
            </div>
            <ActionBar />
        </header>
    );
}