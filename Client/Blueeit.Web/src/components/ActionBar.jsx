import "./ActionBar.css"

export default function ActionBar() {
    return (
        <>
            <div className="blueeit-actionbar-background">
                <div className="blueeit-page-content">
                    <nav className="blueeit-actionbar-nav">
                        <div className="blueeit-actionbar-nav-list">
                            <a href="/forums" className="blueeit-actionbar-item">New Post</a>
                            <a href="/forums" className="blueeit-actionbar-item">Search</a>
                        </div>
                    </nav>
                </div>
            </div>
        </>
    );
}