import "./Block.css";

export default function Block({children}) {
    return (
        <>
            <div className="blueeit-block">
                {children}
            </div>
        </>
    );
}