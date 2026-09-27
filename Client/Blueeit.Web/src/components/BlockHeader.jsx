import "./BlockHeader.css";

export default function BlockHeader({children}) {
    return (
        <>
            <div className="blueeit-block-header">
                <h2>
                    {children}
                </h2>
            </div>
        </>
    );
}